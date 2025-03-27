using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PatientInfoApiClient : MonoBehaviour
{
    public WebClient webClient;

    public async Awaitable<IWebRequestReponse> ReadPatientsInformation()
    {
        string route = "/environments";

        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(route);
        return ParsePatientInfoListResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> CreatePatientInfo(PatientInfo patientInfo)
    {
        string route = "/environments";
        string data = JsonUtility.ToJson(patientInfo);

        IWebRequestReponse webRequestResponse = await webClient.SendPostRequest(route, data);
        return ParsePatientInfoResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> DeletePatientInfo(string Id)
    {
        string route = "/environments/" + Id;
        return await webClient.SendDeleteRequest(route);
    }

   

    private IWebRequestReponse ParsePatientInfoResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                PatientInfo patientInfo = JsonUtility.FromJson<PatientInfo>(data.Data);
                WebRequestData<PatientInfo> parsedWebRequestData = new WebRequestData<PatientInfo>(patientInfo);
                return parsedWebRequestData;
            default:
                return webRequestResponse;
        }
    }

    private IWebRequestReponse ParsePatientInfoListResponse(IWebRequestReponse webRequestResponse)
    {
        switch (webRequestResponse)
        {
            case WebRequestData<string> data:
                Debug.Log("Response data raw: " + data.Data);
                List<PatientInfo> patientenInfo = JsonHelper.ParseJsonArray<PatientInfo>(data.Data);
                WebRequestData<List<PatientInfo>> parsedData = new WebRequestData<List<PatientInfo>>(patientenInfo);
                return parsedData;
            default:
                return webRequestResponse;
        }
    }
}