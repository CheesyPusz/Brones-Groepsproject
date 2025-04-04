using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PatientInfoApiClient : MonoBehaviour
{
    public WebClient webClient;

    public async Awaitable<IWebRequestReponse> ReadPatientsInformation()
    {
        string route = "/PatientInfo";

        IWebRequestReponse webRequestResponse = await webClient.SendGetRequest(route);
        return ParsePatientInfoResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> CreatePatientInfo(PatientInfo patientInfo)
    {
        string route = "/PatientInfo";
        string data = JsonUtility.ToJson(patientInfo);

        IWebRequestReponse webRequestResponse = await webClient.SendPostRequest(route, data);
        return ParsePatientInfoResponse(webRequestResponse);
    }

    public async Awaitable<IWebRequestReponse> PutPatientInfo(float PositionX)
    {
        var positionX = PositionX.ToString();
        string route = "/PatientInfo/" + positionX + "/";
        string data = JsonUtility.ToJson(positionX);
        // Parser maakt een lege data aan, er is niks om te versturen
        
        Debug.Log(data);
        Debug.Log(positionX);

        return await webClient.SendPutRequest(route, data);
        //IWebRequestReponse webRequestResponse = await webClient.SendPutRequest(route, data);
        //return ParsePatientInfoResponse(webRequestResponse);
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