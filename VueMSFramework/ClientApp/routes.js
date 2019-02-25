import HomeIndex from 'components/home/index.vue'
import About from 'components/home/about.vue'
import Activity from 'components/home/activity.vue'

import System from 'components/system/index.vue'
import Table from 'components/table/index.vue'
import Fukuri from 'components/fukuri/index.vue'
import Auth from 'components/auth/index.vue'
import Account from 'components/account/index.vue'
import Option from 'components/option/index.vue'
import MSection from 'components/msection/index.vue'

export const routes = [
    { path: '/home', redirect: '/' },
    { path: '/', component: HomeIndex, display: 'home', style: 'glyphicon glyphicon glyphicon-user' },
    { path: '/about', component: About, display: 'aeLaboについて', style: 'glyphicon glyphicon glyphicon-user' },
    { path: '/system', component: System, display: 'システム', style: 'glyphicon glyphicon glyphicon-user' },
    { path: '/table', component: Table, display: 'テーブル', style: 'glyphicon glyphicon glyphicon-user' },
    { path: '/fukuri', component: Fukuri, meta: { requiresAuth: true }, display: '福利計算', style: 'glyphicon glyphicon glyphicon-user' },
    { path: '/msection', component: MSection, display: 'マルチセクション', style: 'glyphicon glyphicon glyphicon-user' },
    { path: '/activity', component: Activity, display: '活動状況', style: 'glyphicon glyphicon glyphicon-user' },
    { path: '/auth', component: Auth, display: '認証', style: 'glyphicon glyphicon glyphicon-user' },
    { path: '/account', component: Account, display: 'アカウント管理', style: 'glyphicon glyphicon glyphicon-user' },
    { path: '/option', component: Option, display: 'オプション', style: 'glyphicon glyphicon glyphicon-user' },
]
