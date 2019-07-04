namespace Supermarket.API.Domain.Service.Communication
{
    public class GenericResponse<T>  : BaseResponse
    {
        public T Resource { get; private set; }
        
        public GenericResponse(bool success, string message, T resource ) : base(success, message)
        {
            Resource = resource;
        }
        
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="resource">Saved resource.</param>
        /// <returns>Response.</returns>
        public GenericResponse(T resource) : this(true, string.Empty, resource)
        { }
        
        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public GenericResponse(string message) : this(false, message, default(T))
        { }
    }
}