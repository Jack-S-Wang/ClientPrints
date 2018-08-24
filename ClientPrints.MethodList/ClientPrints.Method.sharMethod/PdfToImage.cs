using Spire.Pdf;
using System.Collections.Generic;
using System.Drawing;

namespace ClientPrintsMethodList.ClientPrints.Method.sharMethod
{
    public class PdfToImage
    {
        /// <summary>
        /// 将本地的pdf图片转换为bitmap图片
        /// </summary>
        /// <param name="FilePath">pdf图片的位置</param>
        /// <returns></returns>
        public List<Bitmap> getImage(string FilePath)
        {
            List<Bitmap> li = new List<Bitmap>();
            PdfDocument pd = new PdfDocument();
            pd.LoadFromFile(FilePath, FileFormat.PDF);
            for(int i = 0; i < pd.Pages.Count; i++)
            {
                Bitmap img = pd.SaveAsImage(i) as Bitmap;
                li.Add(img);
            }
            return li;
        }
    }
}
