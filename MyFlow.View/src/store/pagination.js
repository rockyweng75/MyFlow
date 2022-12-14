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
    setPagination: (state, data) => {
        state.pagination = {
            pageIndex: data[0].PageIndex,
            pageSize: data[0].PageSize,
            total: data[0].TotalCount
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