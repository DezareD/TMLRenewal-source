/* TIPPY JS */

export function CreateTippyElement(element) {
    element.forEach((tooltip) => {
        tippy.delegate("body", {
            theme: tooltip.toolTipStyles,
            target: tooltip.elementName,
            content: tooltip.innerHtmlText,
            allowHTML: true,
            interactive: tooltip.isInteractive,
            maxWidth: tooltip.width
        });
    })
}