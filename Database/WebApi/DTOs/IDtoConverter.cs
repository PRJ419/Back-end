namespace WebApi.DTOs
{
    public interface IDtoConverter<T,Y>
    {
        Y ToDto(T fromObject);
    }
}