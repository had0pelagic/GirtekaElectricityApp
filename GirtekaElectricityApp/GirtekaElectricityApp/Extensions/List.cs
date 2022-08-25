namespace GirtekaElectricityApp.Extensions
{
    public static class List
    {
        /// <summary>
        /// Returns list type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <returns></returns>
        public static string GetListType<T>(this List<T> list)
        {
            return typeof(T).Name;
        }
    }
}
