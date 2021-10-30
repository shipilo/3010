namespace Met_3010
{
	class Song
	{
		string name;
		string author;
		Song previous;
		public Song()
		{

		}
		public Song(string Name, string Author)
		{
			name = Name;
			author = Author;
			previous = null;
		}
		public Song(string Name, string Author, Song Previous)
		{
			name = Name;
			author = Author;
			previous = Previous;
		}
	}
}
