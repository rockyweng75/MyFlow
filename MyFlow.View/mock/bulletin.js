import dayjs from 'dayjs'

const addDate = (num) => {
  return dayjs().add(num , 'days').toDate()
}
const mock = [
  {
    url: '/Bulletin/:id',
    method: 'get',
    response: ({ query, body, headers }) => {
      let index = parseInt(query.id)
      return { 
        Id: body.Id,
        From: '系統測試員',
        BeginDate: addDate(index * -1),
        EndDate:  addDate(index + 6),
        Title: '公告' + (index + 1),
        Content: '<div>測試公告內容</div>',
        Files: []
      }
    }
  },
  {
    url: '/Bulletins',
    method: 'get',
    response: ({ query, body, headers }) => {
      var result = []

      for(let i = 1; i <=parseInt(query.pageSize); i++){
        let index = i;
        if(parseInt(query.pageIndex) > 1){
          index = i + ((parseInt(query.pageIndex) - 1) * parseInt(query.pageSize))
        }

        result.push(
          { 
            Id: index,
            BeginDate: addDate(index * -1),
            // EndDate:  moment().add((i * -1) + 2, 'days'),
            Title: '公告' + (index + 1),
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