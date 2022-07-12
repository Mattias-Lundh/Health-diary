import { authToken } from "../Store/Store";

let domain = "https://localhost:44331/";
let token;

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
export const getCampaigns = async (appUserId, callback) => {
  authToken.subscribe((jwt) => (token = jwt));

  let response = await fetch(domain + "api/campaign?AppUserId=" + appUserId, {
    method: "GET",
    headers: {
      Authorization: token,
      Accept: "application/json",
    },
    credentials: "include",
  }).then((res) => {
    return res.json();
  });

  callback(response);
};

export const createCampaign = (campaign, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/campaign", {
    method: "POST",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(campaign),
    credentials: "include",
  })
    .then((response) => response.json())
    .then((data) => callback(data));
};

export const updateCampaign = (campaign, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/campaign", {
    method: "PUT",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(campaign),
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};

export const deleteCampaign = (id, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/campaign?id=" + id, {
    method: "DELETE",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};

// consumption

export const getConsumptions = async (campaignId, callback) => {
  authToken.subscribe((jwt) => (token = jwt));

  let response = await fetch(
    domain + "api/consumption?campaignId=" + campaignId,
    {
      method: "GET",
      headers: {
        Authorization: token,
        Accept: "application/json",
      },
      credentials: "include",
    }
  ).then((res) => {
    return res.json();
  });

  callback(response);
};

export const createConsumption = (consumption, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/consumption", {
    method: "POST",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(consumption),
    credentials: "include",
  })
    .then((response) => response.json())
    .then((data) => callback(data));
};

export const updateConsumption = (consumption, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/consumption", {
    method: "PUT",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(consumption),
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};

export const deleteConsumption = (id, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/consumption?id=" + id, {
    method: "DELETE",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};

// excersise

export const getExcersises = async (campaignId, callback) => {
  authToken.subscribe((jwt) => (token = jwt));

  let response = await fetch(
    domain + "api/excersise?campaignId=" + campaignId,
    {
      method: "GET",
      headers: {
        Authorization: token,
        Accept: "application/json",
      },
      credentials: "include",
    }
  ).then((res) => {
    return res.json();
  });

  callback(response);
};

export const createExcersise = (excersise, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/excersise", {
    method: "POST",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(excersise),
    credentials: "include",
  })
    .then((response) => response.json())
    .then((data) => callback(data));
};

export const updateExcersise = (excersise, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/excersise", {
    method: "PUT",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(excersise),
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};

export const deleteExcersise = (id, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/excersise?id=" + id, {
    method: "DELETE",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};

// consumption templates
export const getConsumptionTemplates = async (campaignId, callback) => {
  authToken.subscribe((jwt) => (token = jwt));

  let response = await fetch(
    domain + "api/ConsumptionTemplate?campaignId=" + campaignId,
    {
      method: "GET",
      headers: {
        Authorization: token,
        Accept: "application/json",
      },
      credentials: "include",
    }
  ).then((res) => {
    return res.json();
  });

  callback(response);
};

export const createConsumptionTemplate = (consumptionTemplate, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/ConsumptionTemplate", {
    method: "POST",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(consumptionTemplate),
    credentials: "include",
  })
    .then((response) => response.json())
    .then((data) => callback(data));
};

export const updateConsumptionTemplate = (consumptionTemplate, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/ConsumptionTemplate", {
    method: "PUT",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(consumptionTemplate),
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};

export const deleteConsumptionTemplate = (id, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/ConsumptionTemplate?id=" + id, {
    method: "DELETE",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};

// excersise templates
export const getExcersiseTemplates = async (campaignId, callback) => {
  authToken.subscribe((jwt) => (token = jwt));

  let response = await fetch(
    domain + "api/ExcersiseTemplate?campaignId=" + campaignId,
    {
      method: "GET",
      headers: {
        Authorization: token,
        Accept: "application/json",
      },
      credentials: "include",
    }
  ).then((res) => {
    return res.json();
  });

  callback(response);
};

export const createExcersiseTemplate = (excersiseTemplate, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/ExcersiseTemplate", {
    method: "POST",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(excersiseTemplate),
    credentials: "include",
  })
    .then((response) => response.json())
    .then((data) => callback(data));
};

export const updateExcersiseTemplate = (excersiseTemplate, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/ExcersiseTemplate", {
    method: "PUT",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(excersiseTemplate),
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};

export const deleteExcersiseTemplate = (id, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/ExcersiseTemplate?id=" + id, {
    method: "DELETE",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};

// meals

export const getMeals = async (campaignId, callback) => {
  authToken.subscribe((jwt) => (token = jwt));

  let response = await fetch(domain + "api/meal?campaignId=" + campaignId, {
    method: "GET",
    headers: {
      Authorization: token,
      Accept: "application/json",
    },
    credentials: "include",
  }).then((res) => {
    return res.json();
  });

  callback(response);
};

export const createMeal = (meal, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/meal", {
    method: "POST",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(meal),
    credentials: "include",
  })
    .then((response) => response.json())
    .then((data) => callback(data));
};

export const updateMeal = (meal, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/meal", {
    method: "PUT",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(meal),
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};

export const deleteMeal = (id, callback) => {
  authToken.subscribe((jwt) => (token = jwt));
  fetch(domain + "api/meal?id=" + id, {
    method: "DELETE",
    headers: {
      Authorization: token,
      "Content-Type": "application/json",
    },
    credentials: "include",
  })
    .then((response) => {
      return response.text();
    })
    .then((data) => {
      callback(data);
    });
};
