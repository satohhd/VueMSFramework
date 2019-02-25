import 'babel-polyfill'

import Vue from 'vue'
import BootstrapVue from 'bootstrap-vue'
import axios from 'axios'
import moment from 'vue-moment'
import router from './router'
import store from './store'

import Apps from 'components/regist-tags.vue'
var jwt = require('jsonwebtoken');

Vue.prototype.$http = axios;
Vue.prototype.$jwt = jwt;
Vue.use(BootstrapVue);
Vue.use(moment);

import VueChartkick from 'vue-chartkick'
import Highcharts from 'highcharts'
Vue.use(VueChartkick, { adapter: Highcharts })

const app = new Vue({
    store,
    router,
    ...Apps,
});

export {
    app,
    router,
    store,
}
