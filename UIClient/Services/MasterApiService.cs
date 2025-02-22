namespace UIClient.Services;

public class MasterApiService(
    UsersApiService usersApiService,
    JobTitlesApiService jobTitlesApiService,
    DepartmentsApiService departmentsApiService,
    CabinetsApiService cabinetsApiService,
    SoftwaresApiService softwaresApiService,
    HardwaresApiService hardwaresApiService,
    ComplexesHardwareApiServices complexesHardwareApiService)
{
    public UsersApiService UsersApiService { get; } = usersApiService;
    public JobTitlesApiService JobTitlesApiService { get; } = jobTitlesApiService;
    public DepartmentsApiService DepartmentsApiService { get; } = departmentsApiService;
    public CabinetsApiService CabinetsApiService { get; } = cabinetsApiService;
    public SoftwaresApiService SoftwaresApiService { get; } = softwaresApiService;
    public HardwaresApiService HardwaresApiService { get; } = hardwaresApiService;
    public ComplexesHardwareApiServices ComplexesHardwareApiService { get; } = complexesHardwareApiService;
}
