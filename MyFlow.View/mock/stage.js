const testCases = [
  {
    "Id": 1,
    "OrderId": 1,
    "FlowId": 1,
    "StageName": "申請人填單",
    "Deadline": "",
    "Target": "",
    "TargetParams": "",
    "ActionFormList": [],
    "StageRouteList": [],
    "StageValidationList": [],
    "StageJobList": []
  },

];
  
const mock = [
  {
      url: '/api/Stage/Flow/:id',
      method: 'get',
      response: ({ query ,body }) => {
        return testCases;
      },
  },
  {
      url: '/api/Stage/:id',
      method: 'get',
      timeout: 2000,
      response: ({ query, body, headers }) => {
        return testCases[0];
    },
  }
]
  
  export default mock