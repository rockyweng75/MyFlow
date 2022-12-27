const testCases = [
  {
    FlowId: 1,
    ApplyId: 1,
    DataId: 1,
    CurrentStage: 1,
    FlowName: '請假單',
    StageName: '申請人',
    ApplyUser: 'Test',
    Title: 'Test申請請假單',
    ApplyDate: new Date(),
    Deadline: new Date() + 2,
    StageStatus: '',
    ApplyStatus: '',
    SubmitDate: new Date(),
  },
];

const pager = {
    PageIndex: 1,
    PageSize: 10,
    Total: 0
};

  
const mock = [
  {
    url: '/Load/:id',
    method: 'get',
    response: ({ query ,body }) => {
      return {
          ApplyData: {
            ApplyUser: 'TestUser',
            ApplyDate: new Date(),
            ApplyReason: '1',
          },
          ApproveData: {

          },
          ActionForms: [
            {
              ActionType: 1,
              ButtonName: '同意',
            },
            {
              ActionType: 2,
              ButtonName: '不同意',
            }
          ]
      };
    },
  },
  {
    url: '/LoadProcessHistory/:id',
    method: 'get',
    response: ({ query ,body }) => {
      return [
        {
          Id: 1,
          FlowId: 1,
          StageId: 1,
          ApplyId: 1,
          FlowName: '請假單',
          StageName: '申請者送出',
          UserId: 'TestUser',
          UserName: 'TestUser',
          FormData: {},
          CloseDate: new Date() + 7
        }
      ];
    },
  },
]
  
export default mock