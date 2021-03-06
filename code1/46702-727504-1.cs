    public partial class Form1 : Form
    {
        private readonly WordFinder _WordFinder;
        public Form1()
        {
            InitializeComponent();
            _WordFinder = new WordFinder();
            _WordFinder.WordFound += _WordFinder_WordFound;
            _WordFinder.NoWordsFound += _WordFinder_NoWordsFound;
        }
        private void _WordFinder_WordFound(object sender, WordFoundEventHandler e)
        {
            // Add item to the list here.
            foundWordsListBox.Items.Add(e.FoundWord);
        }
        private void _WordFinder_NoWordsFound(object sender, EventArgs e)
        {
            MessageBox.Show("No words found!");
        }
        private void findWordsButton_Click(object sender, EventArgs e)
        {
            _WordFinder.FindWords(/* pass file name here */);
        }
    }
