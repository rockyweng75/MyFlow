import dayjs from 'dayjs'

const addDate = (num) => {
  return dayjs().add(num , 'days').toDate()
}
const mock = [
  {
    url: '/Bulletins',
    method: 'get',
    response: ({ query, body, headers }) => {
      var result = []
      for(var i = 1; i <=parseInt(query.pageSize); i++){
        result.push(
          { 
            BeginDate: addDate(i * -1),
            // EndDate:  moment().add((i * -1) + 2, 'days'),
            Title: '公告' + (i + 1),
            Content: ''
          }
        );
      }
      return { 
        Data: result,
        Pager: {
          PageIndex: parseInt(query.pageIndex),
          PageSize: parseInt(query.pageSize),
          TotalCount: 30
        }
      }
    },
  }
]
  
export default mock