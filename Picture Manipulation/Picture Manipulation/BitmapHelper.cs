namespace System.Drawing
{
    public class BitmapHelper
    {
        #region Methods
        public static Bitmap Load(string filename, int width=0, int height=0)
        {
            Bitmap bmp = null;

            try
            {
                if (width > 0 && height > 0)
                {
                    bmp = new Bitmap(Image.FromFile(filename), width, height);
                }
                else
                {
                    bmp = new Bitmap(filename);
                }
            }
            catch (Exception ex)
            {
               System.Diagnostics.Trace.WriteLine("ERROR: Failed to load bitmap " + filename);
               throw ex;
            }

            return bmp;
        }
        #endregion
    }
}
