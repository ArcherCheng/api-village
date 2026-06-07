using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services;

public interface IMa1MasterService : IApiBaseService
{
    Task<Ma1Master?> GetMa1MasterAsync(string teamId);
    Task<List<Ma2MasterEducation>> GetMa2MasterEducationListAsync(string teamId);
    Task<List<Ma2MasterExperience>> GetMa2MasterExperienceListAsync(string teamId);
    Task<List<Ma2MasterPolicy>> GetMa2MasterPolicyListAsync(string teamId);
    Task<List<Ma2MasterPhoto>> GetMa2MasterPhotoListAsync(string teamId);
    Task<List<Ma2MasterPartner>> GetMa2MasterPartnerListAsync(string teamId);


    // Task<List<Tm1MasterSkill>> GetTm1MasterSkillAsync(string teamId);
    // Task<List<Tm1MasterLanguage>> GetTm1MasterLanguageAsync(string teamId);
    // Task<List<Tm1MasterCertificate>> GetTm1MasterCertificateAsync(string teamId);
    // Task<List<Tm1MasterAward>> GetTm1MasterAwardAsync(string teamId);
    // Task<List<Tm1MasterProject>> GetTm1MasterProjectAsync(string teamId);
    // Task<List<Tm1MasterVideo>> GetTm1MasterVideoAsync(string teamId);
    // Task<List<Tm1MasterFile>> GetTm1MasterFileAsync(string teamId);
    // Task<List<Tm1MasterLink>> GetTm1MasterLinkAsync(string teamId);
    // Task<List<Tm1MasterContact>> GetTm1MasterContactAsync(string teamId);
    // Task<List<Tm1MasterFamily>> GetTm1MasterFamilyAsync(string teamId);
    // Task<List<Tm1MasterHobby>> GetTm1MasterHobbyAsync(string teamId);
    // Task<List<Tm1MasterInterest>> GetTm1MasterInterestAsync(string teamId);
    // Task<List<Tm1MasterReference>> GetTm1MasterReferenceAsync(string teamId);
    // Task<List<Tm1MasterResume>> GetTm1MasterResumeAsync(string teamId);
    // Task<List<Tm1MasterResume>> GetTm1MasterResumeAsync(string teamId, Guid userId);
    // Task<List<Tm1MasterResume>> GetTm1MasterResumeAsync(string teamId, string userId);
    // Task<List<Tm1MasterResume>> GetTm1MasterResumeAsync(string teamId, string userId, string resumeId);
    // Task<List<Tm1MasterResume>> GetTm1MasterResumeAsync(string teamId, Guid userId, string resumeId);
    // Task<List<Tm1MasterResume>> GetTm1MasterResumeAsync(string teamId, Guid userId, Guid resumeId);
}
