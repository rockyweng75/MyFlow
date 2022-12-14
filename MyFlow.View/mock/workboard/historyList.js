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
        url: '/api/ApplyHistory',
        method: 'get',
        response: ({ query ,body }) => {
        return {
            Data: testCases,
            Pager: pager
        };
        },
    },
]
    
export default mock
