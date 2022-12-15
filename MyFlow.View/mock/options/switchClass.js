const testCases = [
    { Key: '全過', Value: "AllPass" },
];

const mock = [
    {
        url: '/api/SwitchClass',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock


