using System.Collections.Generic;
using Test.App.Shop.Domain.Exceptions;

namespace Test.App.Shop.Api.Dtos;

public class ResponseDto<T>
{
    public bool Success { get; }
    public T Data { get; }
    public List<ExceptionNotification> Errors { get; }

    public ResponseDto(T data)
    {
        Success = true;
        Data = data;
    }

    public ResponseDto(List<ExceptionNotification> errors)
    {
        Success = false;
        Errors = errors;
    }
}
