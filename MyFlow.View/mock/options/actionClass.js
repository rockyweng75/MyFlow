const testCases = 
    {
        送出: 0,
        同意: 1,
        不同意: 2,
        取消: 3,
        轉送: 4,
        關閉: 5
    };

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


