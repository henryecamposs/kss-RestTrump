using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace RestTrump
{
    enum enuPositionAction
    {
        First,
        Back,
        Next,
        Last
    }

    public partial class frmMonitorvlaView : Form
    {
        int indexImage = 0;
        FeedInstagram_Collection FeedsInstagram;
        List<ItemImage> lstItemImage;
        public frmMonitorvlaView(FeedInstagram_Collection feeds)
        {
            InitializeComponent();
            FeedsInstagram = feeds;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        void movePosition(enuPositionAction action)
        {
            if (lstItemImage != null)
            {
            btnSrchPrev.Enabled = true;
            btnSrchNext.Enabled = true;
            ItemImage itemImage;
            pictureBox1.Image = null;
            lblSrchEncontrados.Text = "";
            lblFeed.Text = "";

            switch (action)
            {
                case enuPositionAction.First:
                    indexImage = 0;
                    break;
                case enuPositionAction.Back:
                    indexImage = indexImage == 0 ? 0 : indexImage - 1;
                    break;
                case enuPositionAction.Next:
                    indexImage = indexImage == lstItemImage.Count -1 ? lstItemImage.Count-1 : indexImage + 1;
                    break;
                case enuPositionAction.Last:
                    indexImage = lstItemImage.Count;
                    break;
            }
            if (indexImage == 0)
                btnSrchPrev.Enabled = false;
            if (indexImage == lstItemImage.Count-1)
                btnSrchNext.Enabled = false;
            itemImage = lstItemImage[indexImage];
            lblFeed.Text = itemImage.Feed.Substring(0, itemImage.Feed.IndexOf(",", 0));
            lblSrchEncontrados.Text = string.Format("{0} de {1}", indexImage + 1, lstItemImage.Count);
            pictureBox1.Image = itemImage.Media;
            pictureBox1.Update();

            }
        }
        private void btnSrchNext_Click(object sender, EventArgs e)
        {
            //Siguiente
            movePosition(enuPositionAction.Next);
        }

        private void btnSrchPrev_Click(object sender, EventArgs e)
        {
            //Anterior
            movePosition(enuPositionAction.Back);
        }

        private void frmMonitorvlaView_Load(object sender, EventArgs e)
        {
            if (FeedsInstagram != null)
            {
int i = 0;
            foreach (FeedInstagram item in FeedsInstagram)
            {
                try
                {

                    List<string> str = Program.WrapText(item.Edge_media_to_caption, 250, "Arial", 10);
                    string str1 = string.Join("\n", str);
                    if (str1.StartsWith(Program.BuscarPor))
                    {
                        if (lstItemImage == null)
                            lstItemImage = new List<ItemImage>();
                        lstItemImage.Add(new ItemImage(str1, LoadPicture(item.Display_url)));
                    }
                    i += 1;
                }
                catch (Exception ex)
                {
                    ksslib.clsUtilErrors.Manejador_errores(ex);
                }

            }
            movePosition(enuPositionAction.First);
            }
            else
            {
                MessageBox.Show("Cuenta Instagram no HALLADA");
            }
            
        }
        private class ItemImage
        {
            public string Feed;
            public Image Media;

            public ItemImage(string feed, Image media)
            {
                Feed = feed;
                Media = media;
            }
        }
        #region Image Utilities

        /// <summary>
        /// Loads an image from a URL into a Bitmap object.
        /// Currently as written if there is an error during downloading of the image, no exception is thrown.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Bitmap LoadPicture(string url)
        {
            System.Net.HttpWebRequest wreq;
            System.Net.HttpWebResponse wresp;
            Stream mystream;
            Bitmap bmp;

            bmp = null;
            mystream = null;
            wresp = null;
            try
            {
                wreq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                wreq.AllowWriteStreamBuffering = true;

                wresp = (System.Net.HttpWebResponse)wreq.GetResponse();

                if ((mystream = wresp.GetResponseStream()) != null)
                    bmp = new Bitmap(mystream);
            }
            catch
            {
                // Do nothing... 
            }
            finally
            {
                if (mystream != null)
                    mystream.Close();

                if (wresp != null)
                    wresp.Close();
            }

            return (bmp);
        }

        /// <summary>
        /// Takes in an image, scales it maintaining the proper aspect ratio of the image such it fits in the PictureBox's canvas size and loads the image into picture box.
        /// Has an optional param to center the image in the picture box if it's smaller then canvas size.
        /// </summary>
        /// <param name="image">The Image you want to load, see LoadPicture</param>
        /// <param name="canvas">The canvas you want the picture to load into</param>
        /// <param name="centerImage"></param>
        /// <returns></returns>

        public static Image ResizeImage(Image image, PictureBox canvas, bool centerImage)
        {
            if (image == null || canvas == null)
            {
                return null;
            }

            int canvasWidth = canvas.Size.Width;
            int canvasHeight = canvas.Size.Height;
            int originalWidth = image.Size.Width;
            int originalHeight = image.Size.Height;

            System.Drawing.Image thumbnail =
                new Bitmap(canvasWidth, canvasHeight); // changed parm names
            System.Drawing.Graphics graphic =
                         System.Drawing.Graphics.FromImage(thumbnail);

            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;

            /* ------------------ new code --------------- */

            // Figure out the ratio
            double ratioX = (double)canvasWidth / (double)originalWidth;
            double ratioY = (double)canvasHeight / (double)originalHeight;
            double ratio = ratioX < ratioY ? ratioX : ratioY; // use whichever multiplier is smaller

            // now we can get the new height and width
            int newHeight = Convert.ToInt32(originalHeight * ratio);
            int newWidth = Convert.ToInt32(originalWidth * ratio);

            // Now calculate the X,Y position of the upper-left corner 
            // (one of these will always be zero)
            int posX = Convert.ToInt32((canvasWidth - (image.Width * ratio)) / 2);
            int posY = Convert.ToInt32((canvasHeight - (image.Height * ratio)) / 2);

            if (!centerImage)
            {
                posX = 0;
                posY = 0;
            }
            graphic.Clear(Color.White); // white padding
            graphic.DrawImage(image, posX, posY, newWidth, newHeight);

            /* ------------- end new code ---------------- */

            System.Drawing.Imaging.ImageCodecInfo[] info =
                             ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters;
            encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality,
                             100L);

            Stream s = new System.IO.MemoryStream();
            thumbnail.Save(s, info[1],
                              encoderParameters);

            return Image.FromStream(s);
        }

        #endregion
    }
}
