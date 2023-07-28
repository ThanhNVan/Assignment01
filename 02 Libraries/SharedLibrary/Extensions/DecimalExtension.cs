namespace SharedLibraries.Extensions;

public static class DecimalExtension
{
    #region [ Methods -  ]
    public static string DecimalToDisplayPrice(decimal price) {
        return string.Format("{0:n2} VND", price);
    }
    #endregion
}
