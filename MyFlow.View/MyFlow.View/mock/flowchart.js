import pagination from "./pagination";
const testCases = [
  {
    Id : 1,
    FlowName : "請假單",
    FlowType : 1,
    AdminUser : "PA",
    Target : "Personnel",
    Close : 0,
    TagFormat : "LeaveForm",
    TitleFormat : "LeaveForm"
  }
];
  
const mock = [
  {
      url: '/api/Flowchart',
      method: 'get',
      response: ({ query ,body }) => {
          return {
            ...testCases,
            ...pagination
          };
      },
  },
  {
    url: '/api/Flowchart/Admin',
    method: 'get',
    response: ({ query ,body }) => {
        return {
          ...testCases,
          ...pagination
        };
    },
},
  {
      url: '/api/Flowchart/:id',
      method: 'post',
      timeout: 2000,
      response: ({ query, body, headers }) => {
        return testCases[0];
    },
  }
]
  
  export default mock