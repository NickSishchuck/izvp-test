namespace TestApi.Interfaces
{
    /// <summary>
    /// Provides methods for serializing and deserializing objects to and from JSON format.
    /// </summary>
    public interface IJsonSerializer
    {
        /// <summary>
        /// Converts an object of type T to its JSON string representation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ToJson<T>(T obj);

        /// <summary>
        /// Converts a JSON string to an object of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T FromJson<T>(string json);
    }
}
