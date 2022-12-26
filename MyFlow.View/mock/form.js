
const testCases = [
  {
    "id": 0,
    "name": "申請單",
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
    "name": "簽核單",
    "children": [
      {
        "id": 2,
        "name": "通用簽核單",
        "children": null
      }
    ]
  }
];


const testApplyForm = {
  FlowId: 1,
  FormType: 0,
  FlowName: '請假單',
  FormName: "請假單",
  Items: [
    {
      Id: 0,
      FormId: 0,
      OrderId: 1,
      ItemType: 0,
      ItemTitle: '申請人',
      ItemValue: 'ApplyUser',
      DataRef: 'ApplyUser',
      Required: true,
      Disabled: true
    },
    {
      Id: 1,
      FormId: 0,
      OrderId: 2,
      ItemType: 1,
      ItemTitle: '申請假別',
      ItemValue: 'ApplyReason',
      DataRef: 'ApplyReason',
      Required: true,
      Disabled: false,
    },
    {
      Id: 2,
      FormId: 0,
      OrderId: 2,
      ItemType: 5,
      ItemTitle: '申請日期',
      ItemValue: 'ApplyDate',
      DataRef: 'ApplyDate',
      Required: true,
      Disabled: false,
    },
    {
      Id: 3,
      FormId: 0,
      OrderId: 3,
      ItemType: 7,
      ItemTitle: '請假原因',
      ItemValue: 'ApplyDesc',
      DataRef: 'ApplyDesc',
      Required: false,
      Disabled: false,
    },
    {
      Id: 4,
      FormId: 0,
      OrderId: 3,
      ItemType: 8,
      ItemTitle: '證明文件',
      ItemValue: 'FileList',
      DataRef: '',
      Required: false,
      Disabled: false,
    }
  ],
  ActionForm: [{
    ButtonName: "送出",
    ActionType: 0
  }]
};
  
const mock = [
  {
    url: '/api/Form',
    method: 'get',
    response: ({ query ,body }) => {
      return testCases;
    },
  },
  {
    
      url: '/api/ApplyForm/:id',
      method: 'get',
      response: ({ query ,body }) => {
        return testApplyForm;
      },
  },
  {
    url: '/api/Form/:id',
    method: 'get',
    response: ({ query ,body }) => {
      return {
        FlowId: 1,
        FormType: 0,
        FormName: "通用簽核單",
        StageName: "代理人簽核",
        Items: [
          {
            Id: 0,
            FormId: 0,
            OrderId: 1,
            ItemType: 7,
            ItemTitle: '意見說明',
            ItemValue: 'ApproveMessage',
            DataRef: 'ApproveMessage',
            Required: false,
            Disabled: true
          },
        ],
      };
    },
},
]
  
export default mock