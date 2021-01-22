    internal class Program
    {
        private static void Main(string[] args)
        {
            // Brian and freddie know only how to say Greetings. But when they tour
            // internationally, they will need a translator so when they say Greetings()
            // the appropriate non-English response comes out of their mouth.
            // they need to make use of the adapter pattern:
            // When in Japan:
            ITarget translator = new JapaneseTranslator(new JapaneseSpeaker());
            EnglishMan freddie = new EnglishMan(translator);
            // Freddie greets Tokyo, though he doesn't know a word of Japanese
            Console.WriteLine(freddie.Greetings()); //  "teo torriatte!"
            // when in France:
            ITarget translator2 = new FrenchTranslator(new FrenchSpeaker());
            EnglishMan brian = new EnglishMan(translator2);
            // Brian greets the crowd in Paris, though he doesn't know a word in French
            Console.WriteLine(brian.Greetings()); 
              // "So très charmant my dear! Bonjour"
            // alternatively, the translators can also do the greeting:
            Console.WriteLine(translator.Greetings());  //  "Konichiwa, hisashiburi!"
            Console.WriteLine(translator2.Greetings()); // "Bonjour!"
        }
        /// <summary>
        /// This is the client.
        /// </summary>
        public class EnglishMan : ITarget
        {
            private ITarget target;
            public EnglishMan(ITarget target)
            {
                this.target = target;
            }
            public string Greetings()
            {
                return target.Greetings();
            }
        }
        /// <summary>
        /// The target interface
        /// </summary>
        public interface ITarget
        {
            string Greetings();
        }
        /// <summary>
        /// This is the adaptor
        /// </summary>
        public class JapaneseTranslator : ITarget
        {
            private JapaneseSpeaker japanese;
            public JapaneseTranslator(JapaneseSpeaker japanese)
            {
                this.japanese = japanese;
            }
            public string Greetings()
            {
                return japanese.Konnichiwa();
            }
        }
        /// <summary>
        /// This is the adaptee
        /// </summary>
        public class JapaneseSpeaker
        {
            public JapaneseSpeaker()
            {
            }
            public string Konnichiwa()
            {
                return "Konichiwa, hisashiburi!";
            }
        }
        /// <summary>
        /// This is the adaptor
        /// </summary>
        public class FrenchTranslator : ITarget
        {
            private FrenchSpeaker french;
            public FrenchTranslator(FrenchSpeaker french)
            {
                this.french = french;
            }
            public string Greetings()
            {
                return french.Bonjour();
            }
        }
        /// <summary>
        /// This is the adaptee
        /// </summary>
        public class FrenchSpeaker
        {
            public string Bonjour()
            {
                return "Bonjour!!";
            }
        }
    }
