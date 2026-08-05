namespace Web_Framework.http;

/**
 * This class allows the code to store and extract headers by creating a format can handle the header.
 *
 * Stores the header name and data whilst providing a method to convert a header directly to a string
 * to be sent to the client.
 */
public class HttpHeader
{
    // Allows the struct to have a common reference type might be obsolete right now but if it aint broke don't fix it
    public interface IHttpHeaders<T>
    {
        public string GetName();

        public T GetValue();
    }

    public struct HttpHeaderData<T> : IHttpHeaders<T>
    {
        private T _value;
        private string _name;
        private Func<T, string> _stringifer;

        /// <summary>
        /// Sets up the header object after the class has been instantiated.
        /// Make sure to use this after creating an object
        /// </summary>
        /// <param name="name">The name of the header as a string, this is directly sent to the client.</param>
        /// <param name="value">The value of any type stored in the header</param>
        public void CreateHeader(string name, T value)
        {
            _name = name;
            _value = value;
        }

        /// <summary>
        /// Changes the value of the given type in the header. The type cannot be changed but the value of the header can
        /// be changed using this method
        /// </summary>
        /// <param name="value">The new value to assign to this header</param>
        public void SetValue(T value)
        {
            _value = value;
        }
        
        /// <summary>
        /// Acquire the value stored in the header, maybe usefule when accessing the headers after they have already been
        /// created or editing a header the exists by default.
        /// </summary>
        /// <returns>The given type of this header value as its stored value</returns>
        public T GetValue()
        {
            return _value;
        }

        /// <summary>
        /// Gets the name of the header
        /// </summary>
        /// <returns>String value of the header name</returns>
        public string GetName()
        {
            return _name;
        }

        /// <summary>
        /// As any object can be passed into the objects of this class, it is important that the code know how to
        /// handle the objects it is dealing with and how to convert them to strings so that they cnd be used to send
        /// responses, this may be an inbuild feature of the given object so a default option is also given for that
        /// case
        /// </summary>
        /// <param name="handler">A lambda expression or other expression that returns the string value of the object</param>
        public void SetStringifier(Func<T, string> handler)
        {
            _stringifer = handler;
        }
        
        /// <summary>
        /// This handles the same issue as the above method but is the special case where there may already be
        /// a method can can turn the object into a string.
        /// </summary>
        public void SetStringifier()
        {
            _stringifer= _value => _value.ToString();
        }

        /// <summary>
        /// Overrides the default handling of converting to a string and replaces it with our custom methods.
        /// </summary>
        /// <returns>The string of the value stored in the header.</returns>
        public override string ToString()
        {
            return _stringifer.Invoke(_value);
        }
    }
}