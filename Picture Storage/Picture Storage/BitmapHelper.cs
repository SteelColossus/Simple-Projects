namespace System.Drawing
{
    public class BitmapHelper
    {
        #region Methods
        public static Bitmap Load(string filename)
        {
            Bitmap bmp = null;
            try
            {
                bmp = new Bitmap(filename); 
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
