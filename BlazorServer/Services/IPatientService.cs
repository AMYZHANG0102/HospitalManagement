using BlazorServer.Models;
namespace BlazorServer.Services;

public interface IPatientService
{
    Task<string> EditPatientProfileAsync(EditPatientProfile request);
}