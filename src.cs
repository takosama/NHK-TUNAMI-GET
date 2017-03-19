      private TunamiResult[] GetTsunamiResultFromURL(string URL)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string str = wc.DownloadString(URL);

            HtmlAgilityPack.HtmlDocument d = new HtmlAgilityPack.HtmlDocument();
            d.LoadHtml(str);
            var info = d.DocumentNode.SelectSingleNode(@"//*[@id=""info""]/div/table");
            var n = info.ChildNodes.Select(x => x.InnerHtml);
            List<TunamiResult> rtn = new List<TunamiResult>();
            
            foreach (var s in n)
            {
                TunamiResult r = new TunamiResult();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(s);
                var tag = doc.DocumentNode.ChildNodes;
                int i = -1;
                foreach (var t in tag)
                {
                    i++;
                    var tmp = t.InnerText;
                    tmp = tmp.Split(';').Last();
                    if (i==0)
                    {
                        r.Place = tmp;
                    }
                    if(i==1)
                    {
                        tmp = tmp.Split('日').Last();
                        r.Time = tmp;
                    }
                    if (i == 2)
                    {
                        r.Height = tmp;
                    }
                    else
                    {

                    }
                    Console.Write(tmp);
                    Console.Write(" ");
                }
                Console.WriteLine("");
                rtn.Add(r);
            }

            Console.WriteLine("");

            if (rtn == null)
                return null;
            if (rtn.Count == 0)
                return null;
            rtn.RemoveAt(0);
            return rtn.ToArray();
        }
    }

    struct TunamiResult
    {
        public string Place { get; set; }
        public string Time { get; set; }
        public string Height { get; set; }
        public TunamiResult(string Place,string Time,string Height)
        {
            this.Place = Place;
            this.Time = Time;
            this.Height = Height;
        }
    }
