<template>
    <div class="content">
        <router-view></router-view>
    </div>
</template>

<script>

    import Vue from 'vue'

    import aeChartBar from './@common/ae-chart-bar'
    Vue.component('ae-chart-bar', aeChartBar);

    import VueHighcharts from 'vue2-highcharts'
    Vue.component('highcharts', VueHighcharts);

    import { VueEditor } from 'vue2-editor'
    Vue.component('ae-editor', VueEditor);

    import { GChart } from 'vue-google-charts'
    Vue.component('ae-chart', GChart);

    import aeHeader from './@common/ae-header.vue'
    Vue.component('ae-header', aeHeader);

    import aeMenu from './@common/ae-menu.vue'
    Vue.component('ae-menu', aeMenu);


    import aeFooter from './@common/ae-footer.vue'
    Vue.component('ae-footer', aeFooter);

    import aeContextMenu from './@common/ae-context-menu.vue'
    Vue.component('ae-context-menu', aeContextMenu);

    import aeDialog from './@common/ae-dynamic-dialog.vue'
    Vue.component('ae-dynamic-dialog', aeDialog);

    import aeWindow from './@common/ae-dynamic-window.vue'
    Vue.component('ae-dynamic-window', aeWindow);

    import aeLoading from './@common/ae-loading.vue'
    Vue.component('ae-loading', aeLoading);

    import aeSectionLoading from './@common/ae-section-loading.vue'
    Vue.component('ae-section-loading', aeSectionLoading);

    import aeDynamicSectionForm from './@common/ae-dynamic-section-form.vue'
    Vue.component('ae-dynamic-section-form', aeDynamicSectionForm);

    import aeDynamicSection from './@common/ae-dynamic-section.vue'
    Vue.component('ae-dynamic-section', aeDynamicSection);

    import aeDynamicPage from './@common/ae-dynamic-page.vue'
    Vue.component('ae-dynamic-page', aeDynamicPage);




    //デフォルトタグ
    const default_tag = "ae-tag";

    import aeTagInput from './@common/ae-tags/ae-tag-input.vue'
    Vue.component('ae-tag-input', aeTagInput)

    import aeTagButton from './@common/ae-tags/ae-tag-button.vue'
    Vue.component('ae-tag-button', aeTagButton)

    import aeTagButtons from './@common/ae-tags/ae-tag-buttons.vue'
    Vue.component('ae-tag-buttons', aeTagButtons)

    import aeTagBoard from './@common/ae-tags/ae-tag-board.vue'
    Vue.component('ae-tag-board', aeTagBoard)

    import aeTagCards from './@common/ae-tags/ae-tag-cards.vue'
    Vue.component('ae-tag-cards', aeTagCards)

    import aeTagChart from './@common/ae-tags/ae-tag-chart.vue'
    Vue.component('ae-tag-chart', aeTagChart)

    import aeTagH from './@common/ae-tags/ae-tag-h.vue'
    Vue.component('ae-tag-h', aeTagH)

    import aeTagLabel from './@common/ae-tags/ae-tag-label.vue'
    Vue.component('ae-tag-label', aeTagLabel)

    import aeTagLine from './@common/ae-tags/ae-tag-line.vue'
    Vue.component('ae-ae-tag-line', aeTagLine)

    import aeTagLink from './@common/ae-tags/ae-tag-link.vue'
    Vue.component('ae-tag-link', aeTagLink)

    import aeTagpanel from './@common/ae-tags/ae-tag-panel.vue'
    Vue.component('ae-tag-panel', aeTagpanel)

    import aeTagPre from './@common/ae-tags/ae-tag-pre.vue'
    Vue.component('ae-tag-pre', aeTagPre)

    import aeTagP from './@common/ae-tags/ae-tag-p.vue'
    Vue.component('ae-tag-p', aeTagP)

    import aeTagPager from './@common/ae-tags/ae-tag-pagination.vue'
    Vue.component('ae-tag-pagination', aeTagPager)

    import aeSpace from './@common/ae-tags/ae-tag-space.vue'
    Vue.component('ae-tag-space', aeSpace)

    import aeTagTabs from './@common/ae-tags/ae-tag-tabs.vue'
    Vue.component('ae-tag-tabs', aeTagTabs)

    import aeTagTextBlock from './@common/ae-tags/ae-tag-text-block'
    Vue.component('ae-tag-text-block', aeTagTextBlock)

    import aeTagTable from './@common/ae-tags/ae-tag-table'
    Vue.component('ae-tag-table', aeTagTable)

    import aeSection from './@common/ae-tags/ae-tag-section'
    Vue.component('ae-tag-section', aeSection)

    import aeTagDialog from './@common/ae-tags/ae-tag-dialog'
    Vue.component('ae-tag-dialog', aeTagDialog)

    import aeTagWindow from './@common/ae-tags/ae-tag-window'
    Vue.component('ae-tag-window', aeTagWindow)




    import spaAuthLib from "./spa-auth-lib.js"

    export default {
        mixins: [spaAuthLib],
        data() {
            return {
            }
        },
    }
</script>
