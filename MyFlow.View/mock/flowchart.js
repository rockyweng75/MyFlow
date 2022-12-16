const testCases = [
  {
    "id": 0,
    "name": "差勤",
    "children": [
      {
        "id": 1,
        "name": "請假單",
        "children": null
      }
    ]
  },
  {
    "id": 1,
    "name": "總務",
    "children": [

    ]
  }
];
  
const mock = [
  {
      url: '/api/Flowchart',
      method: 'get',
      response: ({ query ,body }) => {
        return testCases;
      },
  },
  {
    url: '/api/Flowchart/Admin',
    method: 'get',
    response: ({ query ,body }) => {
        return testCases;
    },
  },
  {
      url: '/api/Flowchart/:id',
      method: 'get',
      response: ({ query, body, headers }) => {
        return {
          Id: 1,
          FlowName: '請假單',
          FlowType: 1,
          AdminUser: 'TestUser',
          TagFormat: 'A000',
          TitleFormat: 'A000'
        };
    },
  }
]
  
  export default mock