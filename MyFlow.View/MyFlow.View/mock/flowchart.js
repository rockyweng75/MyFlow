import pagination from "./pagination";
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
      method: 'post',
      timeout: 2000,
      response: ({ query, body, headers }) => {
        return testCases[0];
    },
  }
]
  
  export default mock