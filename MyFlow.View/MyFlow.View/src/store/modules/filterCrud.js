
export const state = () => ({
    dataList:[],
    pager: {
      PageIndex: 1,
      RowNumber: 10,
      Total: 0
    },
    order:{
      ApplyId: 'Descending'
    },
    filter:{
    }
  })
  
export const mutations = {
    setDataList: (state, list) => {
        state.dataList = list
    },
    setPager: (state, pager) => {
        state.pager = pager
    },
    setPageIndex: (state, index) => {
        state.pager.PageIndex = index
    },
    setRowNumber: (state, index) => {
        state.pager.RowNumber = index
    },
    setOrder: (state, order) => {
        state.order[order.prop] = order.method
    },

    setFilter: (state, filter) => {
        state.filter[filter.prop] = {
        Values: {...filter.values},
        Selected: {...filter.selected}
        }
    },
    setFilterValues: (state, value) => {
        state.filter.Values = value;
    }
}

export const getters = {
    dataList:(state, getters)=>{
        return state.dataList
    },
    pageIndex:(state, getters)=>{
        return state.pager.PageIndex
    },
    rowNumber:(state, getters)=>{
        return state.pager.RowNumber
    },
    total:(state, getters)=>{
        return state.pager.Total
    },
    order:(state, getters)=>{
        return state.order
    },
    filter:(state)=>{
        return state.filter
    },
    filterValues:(state)=>{
        return state.filter.Values
    },
    filterSelected:(state)=>{
        return state.filter.Selected
    }
}  