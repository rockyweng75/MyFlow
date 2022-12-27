const state = {
    list: [],
    pagination: {
        pageIndex: 1,
        pageSize: 10,
        total: 0,
    },
}

const mutations = {
    setList: (state, list) => {
        state.list = list
    },
    setPageIndex: (state, index ) =>{
        state.pagination.pageIndex = index
    },
    setPagination: (state, data) => {
        state.pagination = {
            pageIndex: data.PageIndex,
            pageSize: data.PageSize,
            total: data.TotalCount
        }
    }
}

const getters = {
    list: (state) => state.list,
    pagination: (state) => state.pagination
}

export default {
    state,
    getters,
    mutations,
}