using System;
using System.IO;
using System.Collections;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;


namespace Utilities
{
    /// <summary>
    /// 功能：压缩文件  
    /// </summary>
    public class ZipClass
    {
        /// <summary>
        /// 动态压缩一个文件夹中的文件到一个压缩包
        /// </summary>
        /// <param name="fileToZipPath">要压缩文件夹的路径,此处路径一直到文件夹下即包括被压缩文件夹名称</param>
        /// <param name="fileName">要压缩文件夹的名称</param>
        public void zip(string fileToZipPath, string fileName)
        {
            int fileNameIndex=0;
            //生成以临时文件夹用来实现追加压缩功能
            Directory.CreateDirectory("tempDirectory");
            //判断原来压缩的路径是否有压缩文件
            if (File.Exists(fileToZipPath +"\\"+fileName + ".zip"))
            {
                UnZipClass unZipClass = new UnZipClass();               
                unZipClass.UnZip(fileToZipPath +"\\"+fileName + ".zip", "tempDirectory"); 
                File .Delete(fileToZipPath +"\\"+fileName + ".zip");
            }
            DirectoryInfo orgDirInfo = new DirectoryInfo(fileToZipPath);            
            DirectoryInfo tempDirInfo = new DirectoryInfo("tempDirectory");           
            foreach (FileInfo orgFile in orgDirInfo.GetFiles("*.*"))
            {
                if (!File.Exists("tempDirectory" + "\\" + orgFile.Name))
                {
                    orgFile.MoveTo(("tempDirectory" + "\\" + orgFile.Name));
                }
                else
                {
                   do 
                   {
                       fileNameIndex++;
                       string refileName = orgFile.Name + "(" + fileNameIndex.ToString() + ")";
                       if (!File.Exists("tempDirectory" + "\\" + refileName))
                       {
                           orgFile.MoveTo(("tempDirectory" + "\\" + refileName));
                           fileNameIndex = 0;
                           break;
                       }                      
                   }while (true);

                    //loop: 
                    //fileNameIndex ++;
                    //string refileName = orgFile.Name + "(" + fileNameIndex.ToString() + ")";
                    //if (!File.Exists("tempDirectory" + "\\" + refileName))
                    //{
                    //    orgFile.MoveTo(("tempDirectory" + "\\" + refileName));
                    //    fileNameIndex =0;
                    //}
                    //else
                    //{
                    //    goto loop;
                    //}
                }               
            }
            string fileToZip =System .Windows.Forms .Application .StartupPath +"\\tempDirectory";
            string zipedFile = fileToZipPath + "\\" +fileName;
            ZipDir(fileToZip , zipedFile, 9);
            Directory.Delete(fileToZip, true);
        }

        /// <summary>
        /// 自动选择压缩文件或者文件夹
        /// </summary>
        /// <param name="FileToZip">被压缩文件夹（文件）路径包括被压缩文件夹的名称</param>
        /// <param name="ZipedFile">压缩包存放路径，此路径要求包含存放压缩包的文件夹名称，后面再加上“\\zipedFileName”,其中将zipedFileName作为压缩包的名称</param>
        /// <param name="CompressionLevel"></param>
        public void ZipFileOrDir(string FileToZip, string ZipedFile, int CompressionLevel)
        {
            if (System.IO.File.Exists(FileToZip) == true)
            {
                ZipFile(FileToZip, ZipedFile, CompressionLevel);
            }
            if (System.IO.Directory.Exists(FileToZip) == true)
            {
                ZipDir(FileToZip, ZipedFile, CompressionLevel);
            }
        }
        /// <summary>
        /// 压缩单个文件
        /// </summary>
        /// <param name="FileToZip">被压缩的文件路径(包含文件的名称)</param>
        /// <param name="ZipedFile">压缩后存放压缩包的文件夹路径(包含文件夹的名称)，后面再加上“\\zipedFileName”,其中将zipedFileName作为压缩包的名称</param>
        /// <param name="CompressionLevel">压缩率0（无压缩）-9（压缩率最高）</param>
        /// <param name="BlockSize">缓存大小</param>
        public void ZipFile(string FileToZip, string ZipedFile, int CompressionLevel)
        {
            //如果文件没有找到，则报错 
            if (!System.IO.File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("文件：" + FileToZip + "没有找到！");
            }

            if (ZipedFile == string.Empty)
            {
                ZipedFile = Path.GetFileNameWithoutExtension(FileToZip) + ".zip";
            }

            if (Path.GetExtension(ZipedFile) != ".zip")
            {
                ZipedFile = ZipedFile + ".zip";
            }

            ////如果指定位置目录不存在，创建该目录
            //string zipedDir = ZipedFile.Substring(0,ZipedFile.LastIndexOf("\\"));
            //if (!Directory.Exists(zipedDir))
            //    Directory.CreateDirectory(zipedDir);

            //被压缩文件名称
            string filename = FileToZip.Substring(FileToZip.LastIndexOf('\\') + 1);

            System.IO.FileStream StreamToZip = new System.IO.FileStream(FileToZip, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.FileStream ZipFile = System.IO.File.Create(ZipedFile);
            ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
            ZipEntry ZipEntry = new ZipEntry(filename);
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(CompressionLevel);
            byte[] buffer = new byte[2048];
            System.Int32 size = StreamToZip.Read(buffer, 0, buffer.Length);
            ZipStream.Write(buffer, 0, size);
            try
            {
                while (size < StreamToZip.Length)
                {
                    int sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                    ZipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                ZipStream.Finish();
                ZipStream.Close();
                StreamToZip.Close();
            }
        }

        /// <summary>
        /// 压缩文件夹的方法
        /// </summary>
        /// <param name="DirToZip">被压缩文件夹路径包括被压缩文件夹的名称</param>
        /// <param name="ZipedFile">压缩后存放压缩包的文件夹路径(包含文件夹的名称)，后面再加上“\\zipedFileName”,其中将zipedFileName作为压缩包的名称</param>
        /// <param name="CompressionLevel">压缩等级</param>
        public void ZipDir(string DirToZip, string ZipedFile, int CompressionLevel)
        {
            //压缩文件为空时默认与压缩文件夹同一级目录
            if (ZipedFile == string.Empty)
            {
                ZipedFile = DirToZip.Substring(DirToZip.LastIndexOf("\\") + 1);
                ZipedFile = DirToZip.Substring(0, DirToZip.LastIndexOf("\\")) + "\\" + ZipedFile + ".zip";
            }

            if (Path.GetExtension(ZipedFile) != ".zip")
            {
                ZipedFile = ZipedFile + ".zip";
            }

            using (ZipOutputStream zipoutputstream = new ZipOutputStream(File.Create(ZipedFile)))
            {
                zipoutputstream.SetLevel(CompressionLevel);
                Crc32 crc = new Crc32();
                Hashtable fileList = getAllFies(DirToZip);
                foreach (DictionaryEntry item in fileList)
                {
                    FileStream fs = File.OpenRead(item.Key.ToString());
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry(item.Key.ToString().Substring(DirToZip.Length + 1));
                    entry.DateTime = (DateTime)item.Value;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zipoutputstream.PutNextEntry(entry);
                    zipoutputstream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        /// <summary>
        /// 获取所有文件
        /// </summary>
        /// <returns></returns>
        private Hashtable getAllFies(string dir)
        {
            Hashtable FilesList = new Hashtable();
            DirectoryInfo fileDire = new DirectoryInfo(dir);
            if (!fileDire.Exists)
            {
                throw new System.IO.FileNotFoundException("目录:" + fileDire.FullName + "没有找到!");
            }

            this.getAllDirFiles(fileDire, FilesList);
            this.getAllDirsFiles(fileDire.GetDirectories(), FilesList);
            return FilesList;
        }
        /// <summary>
        /// 获取一个文件夹下的所有文件夹里的文件
        /// </summary>
        /// <param name="dirs"></param>
        /// <param name="filesList"></param>
        private void getAllDirsFiles(DirectoryInfo[] dirs, Hashtable filesList)
        {
            foreach (DirectoryInfo dir in dirs)
            {
                foreach (FileInfo file in dir.GetFiles("*.*"))
                {
                    filesList.Add(file.FullName, file.LastWriteTime);
                }
                this.getAllDirsFiles(dir.GetDirectories(), filesList);
            }
        }
        /// <summary>
        /// 获取一个文件夹下的文件
        /// </summary>
        /// <param name="strDirName">目录名称</param>
        /// <param name="filesList">文件列表HastTable</param>
        private void getAllDirFiles(DirectoryInfo dir, Hashtable filesList)
        {
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                filesList.Add(file.FullName, file.LastWriteTime);
            }
        }
    }
}
