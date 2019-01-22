# Star Citizen API Wrapper

This repository provides a C# wrapper around the Star Citizen REST API(s).

Currently only the organisation API is supported https://robertsspaceindustries.com/api/orgs/getOrgs

It accepts `POST` requests with no body to list all organisations with the default sorting of `size_desc`. Alternatively a json payload can be provided in the body with the following example structure:

`{"sort":"size_desc","search":"","commitment":[],"roleplay":[],"size":[],"model":[],"activity":[],"language":[],"recruiting":[],"pagesize":12,"page":1}`

Supported values for each of the properties can be found in the `Imbick.StarCitizen.Api.Models.SearchRequest.cs` file.

Additional APIs I am aware of and may support in the future are:
`/api/leaderboards/getLeaderboard`
`/api/stats/getCrowdfundStats`