import Layout from '@/layout/index.vue'

const asyncRoutes = [
    {
        name: 'flowchart',
        component: Layout,
        path: '/flowchart',
        children: [
            {
                name: 'flowchartIndex',
                path: 'index',
                component: () =>  import('@/views/flowchart/index.vue'),
                meta: { title: '作業管理', icon: 'el-icon-house', affix: true, roles:['admin'] },
            },
            {
                name: 'flowchartEdit',
                path: 'edit/:id',
                component: () =>  import('@/views/flowchart/edit.vue'),
                meta: { title: '編輯設定', icon: 'el-icon-house', affix: true, roles:['admin']  },
                hidden: true
            },
            {
                name: 'createFlowchart',
                path: 'create',
                component: () =>  import('@/views/flowchart/create.vue'),
                meta: { title: '編輯設定', icon: 'el-icon-house', affix: true, roles:['admin']  },
                hidden: true
            },
        ],
    },
    {
        name: 'formManage',
        component: () => import('@/layout/index.vue'),
        path: '/formManage',
        children: [
            {
                name: 'formManageIndex',
                path: 'index',
                component: () =>  import('@/views/formManage/index.vue'),
                meta: { title: '表單管理', icon: 'el-icon-house', affix: true, roles:['admin'] },
            },
            {
                name: 'formManageEdit',
                path: 'edit/:id',
                component: () =>  import('@/views/formManage/edit.vue'),
                meta: { title: '編輯表單', icon: 'el-icon-house', affix: true, roles:['admin'] },
                hidden: true
            },
            {
                name: 'createFormManage',
                path: 'create',
                component: () =>  import('@/views/formManage/create.vue'),
                meta: { title: '建立表單', icon: 'el-icon-house', affix: true, roles:['admin'] },
                hidden: true
            },
        ],
    },
    // {
    //     component: () => import('/@/layout/index.vue'),
    //     path: '/permission',
    //     // roles:['S', 'A'],
    //     children: [
    //         {
    //             name: 'permission',
    //             path: 'index',
    //             component: () =>  import('/@/views/permission/index.vue'),
    //             meta: { title: '授權管理', icon: 'el-icon-house', affix: true, roles:['S', 'A']},
    //         },
    //         {
    //             name: 'permissionEdit',
    //             path: 'edit/:id',
    //             component: () =>  import('/@/views/permission/edit.vue'),
    //             meta: { title: '編輯授權', icon: '', affix: true, roles:['S', 'A'] },
    //             hidden: true
    //         },
    //         {
    //             name: 'createPermission',
    //             path: 'create',
    //             component: () =>  import('/@/views/permission/create.vue'),
    //             meta: { title: '新增授權', icon: '', affix: true, roles:['S', 'A'] },
    //             hidden: true
    //         },
    //     ],
    // },
    // {
    //     component: () => import('/@/layout/index.vue'),
    //     path: '/courFee',
    //     children: [
    //         {
    //             name: 'courFee',
    //             path: 'index',
    //             component: () =>  import('/@/views/courFee/index.vue'),
    //             meta: { title: '學分費管理', icon: 'el-icon-house', affix: true, roles:['S', 'A'] },
    //         },
    //         // {
    //         //     name: 'courFeeEdit',
    //         //     path: 'edit/:id',
    //         //     component: () =>  import('/@/views/courFee/edit.vue'),
    //         //     meta: { title: '編輯學分費', icon: '', affix: true },
    //         //     hidden: true
    //         // },
    //         // {
    //         //     name: 'createPermission',
    //         //     path: 'create',
    //         //     component: () =>  import('/@/views/permission/edit.vue'),
    //         //     meta: { title: '編輯表單', icon: '', affix: true },
    //         //     hidden: true
    //         // },
    //     ],
    // },
    // {
    //     component: () => import('/@/layout/index.vue'),
    //     path: '/jobLog',
    //     // roles:['S', 'A'],
    //     children: [
    //         {
    //             name: 'jobLog',
    //             path: 'index',
    //             component: () =>  import('/@/views/jobLog/index.vue'),
    //             meta: { title: '工作日誌', icon: 'el-icon-house', affix: true, roles:['S', 'A']},
    //         },
    //         {
    //             name: 'jobLogEdit',
    //             path: 'edit/:id',
    //             component: () =>  import('/@/views/jobLog/edit.vue'),
    //             meta: { title: '檢視日誌', icon: '', affix: true, roles:['S', 'A'] },
    //             hidden: true
    //         },
    //         // {
    //         //     name: 'createPermission',
    //         //     path: 'create',
    //         //     component: () =>  import('/@/views/permission/create.vue'),
    //         //     meta: { title: '新增授權', icon: '', affix: true, roles:['S', 'A'] },
    //         //     hidden: true
    //         // },
    //     ],
    // },
    // {
    //     component: () => import('/@/layout/index.vue'),
    //     path: '/eDeadline',
    //     // roles:['S', 'A'],
    //     children: [
    //         {
    //             name: 'deadline',
    //             path: 'index',
    //             component: () =>  import('/@/views/eDeadline/index.vue'),
    //             meta: { title: '時間設定', icon: 'el-icon-house', affix: true, roles:['S', 'A']},
    //         },
    //         {
    //             name: 'deadlineCreate',
    //             path: 'create/:acadYear/:semeType/:flowId',
    //             component: () =>  import('/@/views/eDeadline/create.vue'),
    //             meta: { title: '編輯設定', icon: '', affix: true, roles:['S', 'A'] },
    //             hidden: true
    //         },
    //         {
    //             name: 'deadlineEdit',
    //             path: 'edit/:id',
    //             component: () =>  import('/@/views/eDeadline/edit.vue'),
    //             meta: { title: '編輯設定', icon: '', affix: true, roles:['S', 'A'] },
    //             hidden: true
    //         },
    //     ],
    // },
    // {
    //     component: () => import('/@/layout/index.vue'),
    //     path: '/fileStorage',
    //     children: [
    //         {
    //             name: 'fileStorage',
    //             path: 'fileTable',
    //             component: () =>  import('/@/views/fileStorage/fileTable.vue'),
    //             meta: { title: '檔案管理', icon: 'el-icon-house', affix: true, roles:['S'] },
    //         },
    //     ],
    // },
];

export default asyncRoutes;


