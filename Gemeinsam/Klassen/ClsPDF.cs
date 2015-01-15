using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace Helper
{
    public class ClsPDF
    {
        public struct ClsPDFBorders
        {
            public double Left;
            public double Top;
        }

        private string m_FileName;
        private string m_FontName;
        private int m_Size1;


        private int m_LeftBorder;
        private int m_TopBorder;

        private PdfDocument m_PdfDokument;
        private PdfPage m_PdfPage;
        private XFont m_FontSize1Bold;

        private XSolidBrush m_XBrush;
        private XPen m_XPen;
        private XGraphics m_XGraphics;
        private Rectangle m_Rectangle;

        private List<ClsPDFBorders> m_ListBorders;

        private ClsSingeltonLanguage m_ClsSingeltonLanguage;

        public ClsPDF()
        {
            this.m_ListBorders = new List<ClsPDFBorders>();

            this.m_ClsSingeltonLanguage = ClsSingeltonLanguage.CreateInstance();
            this.m_Rectangle=new Rectangle();

            this.m_XBrush=new XSolidBrush(XBrushes.Red);

            this.m_XPen=new XPen(Color.Red,3);
            this.m_FileName = "pdftest.pdf";
            this.m_FontName = "Arial";
            this.m_Size1 = 22;
            this.m_FontSize1Bold = new XFont(this.m_FontName, this.m_Size1, XFontStyle.Bold);

            this.m_PdfDokument = new PdfDocument();
            this.AddPage();
            this.AddPage(PageSize.A4, PageOrientation.Landscape);

            this.SetPage(0);
            this.m_XGraphics.DrawString("Calibration", this.m_FontSize1Bold, this.m_XBrush, this.m_LeftBorder, this.m_TopBorder);

            SetPage(1);
            this.m_XGraphics.DrawString("Calibration11111", this.m_FontSize1Bold, this.m_XBrush, this.m_LeftBorder, this.m_TopBorder);

            this.m_Rectangle.X = (int)(this.m_XPen.Width / 2.0);
            this.m_Rectangle.Y = (int)(this.m_XPen.Width / 2.0);
            this.m_Rectangle.Width = 594 - 2;
            this.m_Rectangle.Height = 220;

            this.m_XGraphics.DrawRectangle(this.m_XPen, this.m_Rectangle);

            m_PdfDokument.Save(this.m_FileName);

            Process.Start(this.m_FileName);
        }
        private int GetPointY(double cm)
        {
            int retval = (int)(this.m_PdfPage.Height.Point / this.m_PdfPage.Height.Millimeter * cm * 10.0);
            return retval;
        }
        private int GetPointX(double cm)
        {
            int retval = (int)(this.m_PdfPage.Width.Point / this.m_PdfPage.Width.Millimeter * cm * 10.0);
            return retval;
        }

        private void SetPage(int pageNo)
        {
            if (this.m_XGraphics != null)
            {
                this.m_XGraphics.Dispose();
            }
            PdfPage pdf_page = this.m_PdfDokument.Pages[pageNo];
            this.m_XGraphics = XGraphics.FromPdfPage(pdf_page);

            this.m_LeftBorder = this.GetPointX(this.m_ListBorders[pageNo].Left);
            this.m_TopBorder = this.GetPointY(this.m_ListBorders[pageNo].Top);
        }

        private void AddPage(PageSize pageSize = PageSize.A4, PageOrientation pageOrientation = PageOrientation.Portrait, double leftBorder=2, double topBorder=2)
        {
            ClsPDFBorders pdf_borders;
            pdf_borders.Left = leftBorder;
            pdf_borders.Top = topBorder;
            this.m_ListBorders.Add(pdf_borders);
            this.m_PdfPage = new PdfPage();
            this.m_PdfPage.Size = pageSize;
            this.m_PdfPage.Orientation = pageOrientation;

            this.m_PdfDokument.AddPage(this.m_PdfPage);


            SetPage(this.m_PdfDokument.PageCount - 1);
        }

        ~ClsPDF()
        {
        }
    }
}
