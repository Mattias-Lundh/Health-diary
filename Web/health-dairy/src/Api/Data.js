let domain = "https://localhost:44331/";
let accessToken =
  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJhcHBVc2VySWQiOiIyIiwibmJmIjoxNjU2NDg1MDUzLCJleHAiOjE2NTY0ODg2NTMsImlhdCI6MTY1NjQ4NTA1M30.2U_JQVTJFbSVjQqjfo9mlNYCZMqATUj6pvjcTFLKPIY";
// AppUser
export const login = (email, password, callback) => {
  fetch(domain + "api/appUser/login", {
    method: "POST",
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify({ email, password }),
  })
    .then((response) => response.text())
    .then((data) => {
      callback(data);
    });
};

export const register = (email, password, callback) => {
  fetch(domain + "api/appUser/register", {
    method: "POST",
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify({ email, password }),
  })
    .then((response) => response.text())
    .then((data) => {
      callback(data);
    });
};

// Campaigns
export const getCampaigns = (appUserId) => {
  fetch(domain + "api/campaign?AppUserId=" + appUserId, {
    method: "GET",
    headers: {
      Authorization: accessToken,
    },
    credentials: "include",
  })
    .then((response) => response.json())
    .then((data) => console.log(data));
  return {};
};

export const createCampaign = (campaign) => {
  fetch(domain + "api/campaign", {
    method: "POST",
    headers: {
      Authorization: accessToken,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(campaign),
    credentials: "include",
  })
    .then((response) => response.json())
    .then((data) => console.log(data));
  return {};
};

export const getConsumptions = () => {
  return {};
};

export const getExcersises = () => {
  return {};
};

export const getConsumptionTemplates = () => {
  return {};
};

export const getExcersiseTemplates = () => {
  return {};
};

export const getMeals = () => {
  return {};
};
