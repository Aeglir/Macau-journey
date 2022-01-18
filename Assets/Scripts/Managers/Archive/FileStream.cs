using System.Threading.Tasks;
using System.IO;
namespace Managers.Archive
{
    public class FileStream
    {
        #region save
        /// <summary>
        /// 保存存档信息数据
        /// </summary>
        /// <param name="str">存档信息json字符串</param>
        public static async void saveInfo(string str)
        {
            ArchiveChecker.checkedAndgetPath();
            StreamWriter writer = new StreamWriter(ArchiveChecker.getInfoFileName());
            if (writer == null)
            {
                return;
            }
            await writer.WriteAsync(str);
            writer.Close();
        }
        /// <summary>
        /// 保存存档信息
        /// </summary>
        /// <param name="fileName">完整文件名包括后缀不包括文件路径</param>
        /// <param name="str">存档数据json字符串</param>
        public static async void saveData(string fileName, string str)
        {
            ArchiveChecker.checkedAndgetPath();
            StreamWriter writer = new StreamWriter(ArchiveChecker.getFileName(fileName));
            if (writer == null)
            {
                return;
            }
            await writer.WriteAsync(str);
            writer.Close();
        }
        #endregion
        /// <summary>
        /// 从本地文件读取存档信息数据json字符串
        /// </summary>
        /// <returns>存档信息数据json字符串</returns>
        public static async Task<string> getInfo()
        {
            if (!ArchiveChecker.checkInfoFile())
            {
                return null;
            }
            StreamReader reader = new StreamReader(ArchiveChecker.getInfoFileName());
            if (reader == null)
            {
                return null;
            }
            string str = await reader.ReadToEndAsync();
            reader.Close();
            return str;
        }
        /// <summary>
        /// 从本地文件读取存档数据json字符串
        /// </summary>
        /// <param name="fileName">完整文件名包括后缀不包括文件路径</param>
        /// <returns>存档数据json字符串</returns>
        public static async Task<string> getData(string fileName)
        {
            if (!ArchiveChecker.checkDataFile(fileName))
            {
                return null;
            }
            StreamReader reader = new StreamReader(ArchiveChecker.getFileName(fileName));
            if (reader == null)
            {
                return null;
            }
            string str = await reader.ReadToEndAsync();
            reader.Close();
            return str;
        }
    }
}