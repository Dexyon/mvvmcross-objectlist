namespace Dexyon.MvvmCrossObjectList.Proxy {
	[System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
	public class MvxLangAttribute : System.Attribute
	{
		/// <summary>
		/// Gets or sets the text which is read from the TextSource
		/// </summary>
		/// <value>The babel description.</value>
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the text source. Overrides the default 'TextSource'
		/// </summary>
		/// <value>The text provider.</value>
		public string TextSource { get; set; }
	}
}

