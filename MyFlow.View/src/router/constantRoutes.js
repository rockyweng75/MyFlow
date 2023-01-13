import Layout from '@/layout/index.vue'

const constantRoutes = [
    { 
        path: '/:pathMatch(.*)*',
        component: () => import('@/views/404/index.vue'),
        name: 'NotFound'
    },
    {
        path: '/',
        component: Layout,
        redirect: '/home',
        children: [
            {
                path: 'home',
                component: () => import('@/views/home/index.vue'),
                name: 'Home',
                meta: { title: '首頁', icon: 'el-icon-house', affix: true },
            },
            
        ]
    },
    {
        name: 'login',
        path: '/login',
        component: () => import('@/views/login/index.vue'),
    },
    {
        name: 'logout',
        path: '/logout',
        component: () => import('@/views/logout/index.vue'),
    },
    {
        name: '401',
        path: '/401',
        component: () => import('@/views/401/index.vue'),
    },
    {
        path: '/workboard',
        component:  () => import('@/layout/index.vue'),
        meta: { title: '表單申請', icon: 'el-icon-house', affix: true },
        children: [
            {
                name: 'selectForm',
                path: 'selectForm',
                component:  () => import('@/views/selectForm/index.vue'),
                meta: { title: '新增表單', icon: 'el-icon-house', affix: true },        
            },
            {
                name: 'todoList',
                path: 'todoList',
                component: () => import('@/views/workboard/components/todoList.vue'),
                meta: { title: '待辦表單', icon: 'el-icon-house', affix: true },
                // hidden: true
            },
            {
                name: 'waitList',
                path: 'waitList',
                component: () => import('@/views/workboard/components/waitList.vue'),
                meta: { title: '個人即時表單', icon: 'el-icon-house', affix: true },
                // hidden: true
            },
            {
                name: 'historyList',
                path: 'historyList',
                component: () => import('@/views/workboard/components/historyList.vue'),
                meta: { title: '個人申請記錄', icon: 'el-icon-house', affix: true },
                // hidden: true
            },
            {
                name: 'approveList',
                path: 'approveList',
                component: () => import('@/views/workboard/components/approveList.vue'),
                meta: { title: '個人簽辦記錄', icon: 'el-icon-house', affix: true },
                // hidden: true
            },
        ]
    },
    {
        component: Layout,
        path: '/applyForm',
        children: [
            {
                name: 'applyForm',
                path: ':id',
                component: () =>  import('@/views/applyForm/index.vue'),
                meta: { title: '填寫表單', affix: true },
            },
        ],
        hidden: true
    },
    {
        component: Layout,
        path: '/approveForm',
        children: [
            {
                name: 'approveForm',
                path: ':flowid/:stageid/:applyid/:apprid',
                component: () =>  import('@/views/approveForm/index.vue'),
                meta: { title: '審核表單', affix: true },
            },
        ],
        hidden: true
    },
    {
        component: Layout,
        path: '/reviewForm',
        children: [
            {
                name: 'reviewForm',
                path: ':flowid/:stageid/:applyid/:apprid',
                component: () =>  import('@/views/reviewForm/index.vue'),
                meta: { title: '檢視表單', affix: true },
            },
        ],
        hidden: true
    },
    {
        component: Layout,
        path: '/bulletin',
        children: [
            {
                name: 'bulletin',
                path: ':id',
                component: () =>  import('@/views/home/bulletin/detail.vue'),
                meta: { title: '公告事項', affix: true },
            },
        ],
        hidden: true
    }
];

export default constantRoutes;


