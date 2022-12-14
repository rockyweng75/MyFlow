const testApplyForm = {
  FlowId: 1,
  FlowName: "請假單",
  Items: [],
  ActionForm: [{
    ButtonName: "送出",
    ActionType: 0
  }]
};
  
const mock = [
  {
      url: '/api/Form/Apply/:id',
      method: 'get',
      response: ({ query ,body }) => {
        return testApplyForm;
      },
  },
]
  
export default mock