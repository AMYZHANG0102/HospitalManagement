using BlazorServer.Models;
namespace BlazorServer.Services;

public interface IPatientService
{
    Task<string> EditPatientProfileAsync(Patient request);
}