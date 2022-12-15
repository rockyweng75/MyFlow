const testCases = 
[
    {Key: 'Input', Value: 0},
    {Key: 'Select', Value: 1},
    {Key: 'Checkbox', Value: 2},
    {Key: 'Rediobox', Value: 3},
    {Key: 'Label', Value: 4},
    {Key: 'Date', Value: 5},
    {Key: 'DateTime', Value: 6},
]

const mock = [
    {
        url: '/api/ItemType',
        method: 'get',
        response: ({ query ,body }) => {
            return testCases
        }
    }
]
    
export default mock