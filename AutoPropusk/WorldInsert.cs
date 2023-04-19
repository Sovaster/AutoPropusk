using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.IO;

namespace AutoSalon
{
    class WorldInsert
    {
        private FileInfo _fileInfo;
        string Client;

        public WorldInsert(string filename, string client)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Shablon.docx"))
            {
                _fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Shablon.docx");
                Client = client;
            }
            else
            {
                throw new ArgumentException("File not found...");
            }

        }

        internal bool Process(Dictionary<string, string> items)
        {
            Word.Application app = null;
            try
            {

                app = new Word.Application();

                Object file = _fileInfo.FullName;
                Object missing = Type.Missing;
                app.Documents.Open(file);

                foreach (var item in items)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;



                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing, Replace: replace
                        );
                }

                string path = @"C:\Пропуска на парковку";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                string PathDoc = $@"C:\" + "Пропуска на парковку" + "\\" + "Пропуск на " + Client + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".docx";

                Object newFileName = Path.Combine(PathDoc);
                app.ActiveDocument.SaveAs2(newFileName);
                app.ActiveDocument.Close();


                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (app != null) { app.Quit(); }

            }
            return false;
        }
    }
}
