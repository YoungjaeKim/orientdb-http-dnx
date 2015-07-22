namespace OrientDb
{
	public class CommandOptions
    {
		public enum CommandLanguage
		{
			/// <summary>
			/// SQL
			/// </summary>
			Sql,
			/// <summary>
			/// Javascript
			/// </summary>
			Script,
			/// <summary>
			/// Gremlin script
			/// </summary>
			Gremlin
		}

		public CommandLanguage Language { get; set; } = CommandLanguage.Sql;
		public int Limit { get; set; } = 20;
		public FetchPlan FetchPlan { get; set; } = new FetchPlan();
	}
}
