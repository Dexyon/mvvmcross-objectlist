
namespace Dexyon.MvvmCrossObjectList.Proxy {
	[System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
	public class ProxyModelAttribute : System.Attribute
	{
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public string ValueConverter { get; set; }

		/// <summary>
		/// Gets or sets the order.
		/// </summary>
		/// <value>The order.</value>
		public int Order { get; set; }
	}
}

