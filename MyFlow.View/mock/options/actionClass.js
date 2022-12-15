const testCases = [
    {
        'ActionType': 1,
        'Options': {
            '下一關': 'Next',
            '結案': 'Close'
        }
    },
    {
        'ActionType': 2,
        'Options': {
            '退回上一關': 'Previous'
        }
    }
];

const mock = [
    {
        url: '/api/ActionClass',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock


