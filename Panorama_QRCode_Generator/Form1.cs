using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using CODRA.Panorama.Persist;
using System.IO;
using QRCoder;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Panorama_QRCode_Generator
{
    public partial class Form1 : Form
    {
        static IPersistCtrl MyPersist;
        static IPrsApplicationManager MyAppManager;

        List<string> qrCodeList = new List<string>();
        public Form1()
        {
            InitializeComponent();

            MyPersist = new PersistCtrl();
            MyPersist.DesignMode = false;
            MyAppManager = (IPrsApplicationManager)MyPersist;
        }
        private void AddLineStatus(string text)
        {
            textBoxStatus.Text = text + "\x0d\x0a" + textBoxStatus.Text;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            ProtectionType res = 0;
            try
            {
                textBoxStatus.Text = "";
                AddLineStatus("Ouverture de l'application...");
                IPrsApplication MyApp = MyAppManager.Open(@textBoxAppPath.Text, 0);

                if ((MyApp.AccessMode == ProtectionType.WRITE_PROTECTED) || (MyApp.AccessMode == ProtectionType.READWRITE_PROTECTED))
                {
                    res = MyApp.UnlockApplication("", textBoxPassword.Text);

                    if (res != ProtectionType.NOT_PROTECTED)
                    {
                        // Wrong password or read protection ! 
                        AddLineStatus("Erreur de mot de passe !!");
                        return;
                    }
                }

                if (MyApp.Status == ApplicationStatus.APP_OK)
                {
                    AddLineStatus("Application ouverte");
                    textBoxAppPath.Enabled = false;
                    btnSearchApp.Enabled = false;
                    btnGenerate.Enabled = false;
                }

                AddLineStatus("Recherche des QR Code...");

                //Scan unit in search of application QRCode
                if (MyApp.UnitsEx.Count > 0)
                {
                    foreach (IPrsUnit MyUnit in MyApp.UnitsEx)
                    {
                        AddLineStatus("Scan unit " + MyUnit.Name);
                        if (MyUnit.Name != "$_LIBRARY" && MyUnit.Name != "$_SYSTEM" && MyUnit.Name != "$_PARAMETERS" && MyUnit.Name != "$_WORKSTATION")
                            ScanUnit(MyUnit);
                    }
                }

                //Create PDF file with QRCode
                CreatePdfFile(qrCodeList);

                AddLineStatus("Fermeture de l'application...");
                MyAppManager.Close(textBoxAppPath.Text);
                MyApp.Unload();
                MyPersist.Unload();
                AddLineStatus("Application fermée");

                textBoxAppPath.Enabled = true;
                btnSearchApp.Enabled = true;
                btnGenerate.Enabled = true;
            }
            catch (Exception ex)
            {
                string message;

                int iErrorCode = unchecked((int)0x80040005); // initialize with E_FAIL

                ExternalException ExtEx = (ExternalException)ex;

                if (null != ExtEx)
                {
                    iErrorCode = ExtEx.ErrorCode; // get PERSIST Exception if available
                }
                else if (ex is UnauthorizedAccessException)
                {
                    iErrorCode = unchecked((int)0x80070005); // E_ACCESSDENIED
                }
                else if (ex is ArgumentException)
                {
                    iErrorCode = unchecked((int)0x80070057); // E_INVALIDARG
                }
                message = MyPersist.GetErrorText(iErrorCode, 0);

                MessageBox.Show(message, "Erreur ouverture application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MyPersist.Unload();
            }
        }

        private void ScanUnit(IPrsUnit unit)
        {
            if (unit.UnitsEx.Count > 0)
            {
                foreach (IPrsUnit MyUnit in unit.UnitsEx)
                {
                    AddLineStatus("Scan unit " + MyUnit.Name);
                    ScanUnit(MyUnit);

                    if (MyUnit.FullName.StartsWith("/$_LIBRARY") == false)
                        MyUnit.Unload();
                }
            }
            if (unit.ObjectsEx.Count > 0)
            {
                foreach (IPrsObject MyObj in unit.ObjectsEx)
                {
                    AddLineStatus("Scan obj " + MyObj.Name);
                    ScanObject(MyObj);
                }
            }

        }

        private void ScanObject(IPrsObject obj)
        {
            try
            {
                if (obj.get_CollectionEx("Objects").Count > 0)
                {
                    foreach (IPrsObject subObj in obj.get_CollectionEx("Objects"))
                    {
                        AddLineStatus("Scan sub obj " + subObj.Name);
                        ScanObject(subObj);
                    }
                }
            }
            catch
            {

            }
            if (obj.ClassName == "SynoptHMI_Mobile" && obj.ModuleName == "CODRA_Mobile")
            {
                object[] values = null;
                string[] names = null;
                bool[] defaults = null;
                string[] links = null;
                ValueSourceType[] VSTs = null;

                obj.get_ValuesEx(ref values, ref names, ref defaults, ref links, ref VSTs, GetValuesExOptions.GVO_NAMES | GetValuesExOptions.GVO_VALUES);

                for (int i = 0; i < names.Length; i++)
                {
                    if (names[i] == "QRCode")
                    {
                        if (values[i].ToString() != "")
                            qrCodeList.Add(values[i].ToString());
                    }
                }
            }
            
        }

        private void btnSearchApp_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBoxAppPath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void CreatePdfFile(List<string> qrCodes)
        {
            int qrIndex = 0, qrLine = 0;
            //Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "My PDF file";

            //Create empty page
            PdfPage page = document.AddPage();

            //Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //Create a font
            XFont font = new XFont("Verdana", 10, XFontStyle.Bold);

            foreach (string qr in qrCodes)
            {
                int xPoint, yPoint;

                //Set y position for QRCode
                if (qrLine % 2 == 0)
                    yPoint = 440;
                else
                    yPoint = 50;

                //Add page if needed
                if (qrLine % 2 == 0 && qrIndex % 2 == 0 && qrLine != 0)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                }
                    
                //Set x position for QRCode
                if (qrIndex % 2 == 0)
                {
                    xPoint = 0;
                }  
                else
                {
                    xPoint = 290;
                    qrLine++;
                }

                qrIndex++;

                //Generate QRCode
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qr, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(10);

                //Save QRCode in image file
                qrCodeImage.Save("qrCode" + qrIndex + ".bmp");

                //Add image file to PDF file
                using (XImage image = XImage.FromFile("qrCode" + qrIndex + ".bmp"))
                {
                    gfx.DrawImage(image, new XPoint(xPoint, yPoint));
                }
                //Delete image file
                File.Delete("qrCode" + qrIndex + ".bmp");

                //Draw QRCode text
                gfx.DrawString(qr, font, XBrushes.Black, new XRect(xPoint + 100, yPoint + 300, 100, 20), XStringFormats.Center);

            }

            //Open select file dialog
            saveFileDialog1.ShowDialog();
            //Save PDF file
            string filename = saveFileDialog1.FileName;
            document.Save(filename);
            Process.Start(filename);
        }
    }
}
