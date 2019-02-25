import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)


export default new Vuex.Store({
    state: {
        wndStatuses: {},
        wndCount: 0,
        maxWndZIndex: 0,
        viewModel: {},
        signInUser: { authorized: false, user: null },
        lang: null,
        breadcrumb: [],
        loading:0,
   },
    mutations: {
        setWndStatuses: (state, payload) => {
            if (!state.wndStatuses[payload.wndID]) {
                Vue.set(state.wndStatuses, payload.wndID, {
                    zIndex: state.wndCount,
                });
                state.maxWndZIndex = state.wndCount;
                state.wndCount = state.wndCount + 1;
            }
        },
        moveWndToTop: (state, payload) => {
            let oldZIndex = state.wndStatuses[payload.wndID].zIndex;
            state.wndStatuses[payload.wndID].zIndex = state.maxWndZIndex;
            for (let key in state.wndStatuses) {
                if ((state.wndStatuses[key].zIndex > oldZIndex) && (key != payload.wndID)) {
                    state.wndStatuses[key].zIndex -= 1;
                }
            }
        },
        setViewModel: (state, payload) => {
            state.viewModel = payload;
        },
        setSignInUser: (state, payload) => {
            state.signInUser = payload;

        },
        setBreadcrumb: (state, payload) => {
            state.breadcrumb = payload;
        },
        addLoading: (state, payload) => {
            state.loading += payload;
        },
        resetLoading: (state) => {
            state.loading = 0;
        },
        setLang: (state, payload) => {
            state.lang = payload;
        },
    },
    actions: {
        setWndStatuses(context, payload) {
            context.commit('setWndStatuses', payload);
        },
        moveWndToTop(context, payload) {
            context.commit('moveWndToTop', payload);
        },
        setViewModel(context, payload) {
            context.commit('setViewModel', payload);
        },
        setSignInUser(context, payload) {
            context.commit('setSignInUser', payload);
        },
        setBreadcrumb(context, payload) {
            context.commit('setBreadcrumb', payload);
        },
        addLoading(context, payload) {
            context.commit('addLoading', payload);
        },
        resetLoading(context) {
            context.commit('resetLoading');
        },
        setLang(context, payload) {
            context.commit('setLang', payload);
        },
    }
});
