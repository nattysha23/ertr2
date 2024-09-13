namespace Primer_21.Database.Helpers
{
    public class ColumnType
    {
        public const string Date = "timestamp";
        public const string Guid = "uuid";
        public const string String = "nvarchar(Max)";// "varchar"
        public const string Text = "text";
        public const string Bool = "bit";//"bool"
        public const string Int = "int"; //"int4"
        public const string Long = "bigint";// "int8"
        public const string Decimal = "decimal(18,2)"; // "money"
        public const string Double = "float"; //"numeric(9,2)"
    }
}
