# Star Citizen API Wrapper

This repository provides a C# wrapper around the Star Citizen REST API(s).

## Supported APIs from robertsspaceindustries.com

Currently the only supported APIs are:

* the organisation https://robertsspaceindustries.com/api/orgs/getOrgs
* the organisation's members https://robertsspaceindustries.com/api/orgs/getOrgMembers

### Organisations

It accepts `POST` requests with no body to list all organisations with the default sorting of `size_desc`. Alternatively a json payload can be provided in the body with the following example structure:

`{sort:"size_desc",search:"",commitment:[],roleplay:[],size:[],model:[],activity:[],language:[],recruiting:[],pagesize:12,page:1}`

Supported values for each of the properties can be found in the `Imbick.StarCitizen.Api.Models.OrgsSearchRequest.cs` file.

### Organisation's Members

It accepts `POST` requests with at least the symbol of the organisation to get the members. Alternatively a json payload can be provided in the body with the following example structure:

`{symbol:"SWISS",search:"",pagesize:32,page:1}`
`{symbol:"SWISS",search:"Meetsch",pagesize:32,page:1}`
`{symbol:"SWISS",search:"",rank:1,role:1,main_org:1,pagesize:32,page:1}`

Supported values for each of the properties can be found in the `Imbick.StarCitizen.Api.Models.OrgMembersSearchRequest.cs` file.

## Roadmap

Additional APIs may be supported in the future:  
`/api/leaderboards/getLeaderboard`  
`/api/stats/getCrowdfundStats`  
`/api/starmap`  
